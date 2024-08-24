"use client"
import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { z } from "zod"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import { Input } from "@/components/ui/input"
import { updateModule } from "@/services/moduleService"
import { ICommandDto } from "@/abstract/moduleTypes"
import { useDialogContext } from "@/contexts/DialogContext"
import { Switch } from "@/components/ui/switch"
import { IModule } from '@/abstract/moduleTypes';
import { toast } from "sonner"


interface EditFormProps {
    fetchModules: () => void;
    selectedModule: IModule | null;
}

const EditForm: React.FC<EditFormProps> = ({ fetchModules, selectedModule }) => {
    const { formRef, setIsEditAlertDialogOpen } = useDialogContext();

    const formSchema = z.object({
        name: z.string()
            .min(2, 'Имя должно быть не меньше 2 символов')
            .max(60, 'Имя должно быть не больше 60 символов'),
        width: z.preprocess(
            (val) => parseFloat(val as string), 
            z.number()
              .positive("Ширина должна быть положительным числом больше 0")
              .refine((val) => val > 0, { message: "Ширина должна быть больше 0" })
              .transform((val) => Math.round(val * 10) / 10)
        ),
        height: z.preprocess(
            (val) => parseFloat(val as string), 
            z.number()
              .positive("Высота должна быть положительным числом больше 0")
              .refine((val) => val > 0, { message: "Высота должна быть больше 0" })
              .transform((val) => Math.round(val * 10) / 10)
        ),
        isCorner: z.boolean(),
        requiresWater: z.boolean(),
    });

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            name: selectedModule?.name || '',
            width: selectedModule?.width || 0,
            height: selectedModule?.height || 0,
            isCorner: selectedModule?.isCorner || false,
            requiresWater: selectedModule?.requiresWater || false
        },
    });

    const onSubmit = async (values: z.infer<typeof formSchema>) => {
        if (selectedModule) {
            const response = await updateModule(values as ICommandDto, selectedModule.id);
            if (response) {
                setIsEditAlertDialogOpen(false);
                fetchModules();
            }
        } else {
            toast.error("Не получилось добавить")
        }

    };

    return (
        <>
            <Form {...form}>
                <form ref={formRef} onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 pr-4 pl-1 ">
                    <FormField
                        control={form.control}
                        name="name"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Введите название</FormLabel>
                                <FormControl>
                                    <Input {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="width"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Введите Ширину</FormLabel>
                                <FormControl>
                                    <Input type="number" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="height"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Введите Высоту</FormLabel>
                                <FormControl>
                                    <Input type="number" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="isCorner"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>В углу?</FormLabel>
                                <FormControl>
                                    <Switch
                                        className="ml-4"
                                        checked={field.value}
                                        onCheckedChange={field.onChange}
                                    />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="requiresWater"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Нужна вода?</FormLabel>
                                <FormControl>
                                    <Switch
                                        className="ml-4"
                                        checked={field.value}
                                        onCheckedChange={field.onChange}
                                    />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                </form>
            </Form>
        </>
    );
}

export default EditForm;