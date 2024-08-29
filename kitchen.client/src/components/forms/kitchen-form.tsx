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
import { useDialogContext } from "@/contexts/DialogContext"
import { createKitchen } from "@/services/kitchenService"
import { ICommandDto } from "@/abstract/kitchenTypes"
import { useNavigate } from "react-router-dom"

const KitchenForm: React.FC = () => {
    const { formRef, setIsKitchenAlertDialogOpen } = useDialogContext();
    const navigate = useNavigate();

    const coordinateSchema = z.object({
        x: z.preprocess(
            (val) => parseFloat(val as string),
            z.number()
                .refine((val) => val >= 0, { message: "X должен быть >= 0" })
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
        waterPipe: coordinateSchema
    });

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            width: 0,
            height: 0,
            waterPipe: { x: 0, y: 0 },
        },
    });

    const onSubmit = async (values: z.infer<typeof formSchema>) => {
        const response: any = await createKitchen(values as ICommandDto);
        if (response) {
            setIsKitchenAlertDialogOpen(false);
            navigate(`/kitchen/${response.kitchenId}`);
        }
    }

    return (
        <>
            <Form {...form}>
                <form ref={formRef} onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 pr-4 pl-1 ">

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
                        name="waterPipe.x"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Введите X трубы</FormLabel>
                                <FormControl>
                                    <Input type="number" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="waterPipe.y"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Введите Y трубы</FormLabel>
                                <FormControl>
                                    <Input type="number" {...field} />
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

export default KitchenForm;