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
import { useDialogContext } from "@/contexts/DialogContext"
import { addModuleToKitchen } from "@/services/kitchenService"
import { IAddModuleDto } from "@/abstract/kitchenTypes"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Input } from "../ui/input"

interface KitchenModuleFormProps {
    moduleId: string;
    kitchenId: string;
    checkAndFetchKitchen: (kitchenId: string) => Promise<void>;
}

const KitchenModuleForm: React.FC<KitchenModuleFormProps> = ({ moduleId, kitchenId, checkAndFetchKitchen }) => {
    const { formRef, setIsModuleToKitchenAlertDialogOpen } = useDialogContext();

    const coordinateSchema = z.object({
        x: z.preprocess(
            (val) => parseFloat(val as string),
            z.number()
                .refine((val) => val >= 0, { message: "X должен быть >= 0 " })
                .transform((val) => Math.round(val * 10) / 10)
        ),
        y: z.preprocess(
            (val) => parseFloat(val as string),
            z.number()
                .refine((val) => val >= 0, { message: "Y должен быть >= 0" })
                .transform((val) => Math.round(val * 10) / 10)
        ),
    });

    const formSchema = z.object({
        orientation: z.number(),
        coordinate: coordinateSchema
    });

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            orientation: 0,
            coordinate: { x: 0, y: 0 },
        }
    })

    const onSubmit = async (values: z.infer<typeof formSchema>) => {
        const command: IAddModuleDto = {
            ...values,
            moduleId: moduleId,
            kitchenId: kitchenId,
        }
        await addModuleToKitchen(command);
        await checkAndFetchKitchen(kitchenId);
        setIsModuleToKitchenAlertDialogOpen(false)
    }

    return (
        <>
            <Form {...form}>
                <form ref={formRef} onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 pr-4 pl-1 ">
                    <FormField
                        control={form.control}
                        name="coordinate.x"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Введите X модуля</FormLabel>
                                <FormControl>
                                    <Input type="number" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="coordinate.y"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Введите Y модуля</FormLabel>
                                <FormControl>
                                    <Input type="number" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="orientation"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Выберите ориентацию</FormLabel>
                                <Select onValueChange={(value) => field.onChange(parseInt(value))} defaultValue={field.value.toString()}>
                                    <FormControl>
                                        <SelectTrigger>
                                            <SelectValue placeholder="Ориентация" />
                                        </SelectTrigger>
                                    </FormControl>
                                    <SelectContent>
                                        <SelectItem value="0">Горизонтальная</SelectItem>
                                        <SelectItem value="1">Вертикальная</SelectItem>
                                    </SelectContent>
                                </Select>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                </form>
            </Form>
        </>
    );
};

export default KitchenModuleForm;