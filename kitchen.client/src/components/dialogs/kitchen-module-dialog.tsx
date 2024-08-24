import { useDialogContext } from "@/contexts/DialogContext";
import {
    AlertDialog,
    AlertDialogContent,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogCancel
} from "@/components/ui/alert-dialog";
import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
} from "@/components/ui/accordion"
import { LoadingSpinner } from "@/components/ui/loading-spinner";
import { Button } from "@/components/ui/button";
import { useEffect, useState } from "react";
import { IModule } from "@/abstract/moduleTypes";
import { getAllModules } from "@/services/moduleService";
import { ScrollArea } from "@/components/ui/scroll-area";
import KitchenModuleForm from "@/components/forms/kitchen-module-form";

interface KitchenModuleDialogProps {
    checkAndFetchKitchen: (kitchenId: string) => Promise<void>;
    kitchenId: string;
}

const KitchenModuleDialog: React.FC<KitchenModuleDialogProps> = ({ checkAndFetchKitchen, kitchenId }) => {
    const { isModuleToKitchenAlertDialogOpen, setIsModuleToKitchenAlertDialogOpen, triggerSubmit } = useDialogContext();
    const [modules, setModules] = useState<IModule[] | null>(null);

    const fetchModules = async () => {
        const data = await getAllModules();
        if (data) {
            setModules(data);
        }
    }

    useEffect(() => {
        fetchModules();
    }, [])

    return (
        <AlertDialog open={isModuleToKitchenAlertDialogOpen} onOpenChange={setIsModuleToKitchenAlertDialogOpen}>
            <AlertDialogContent className="max-w-[80%]">
                <AlertDialogHeader>
                    <AlertDialogTitle>Добавление модуля к кухне</AlertDialogTitle>
                    <AlertDialogDescription>
                        <Accordion type="single" collapsible>
                            <ScrollArea className="h-[550px]">
                                {modules === null ? (
                                    <LoadingSpinner />
                                ) : (
                                    modules.map(module => (
                                        <AccordionItem key={module.id} value={module.id} className="pr-4">
                                            <AccordionTrigger>
                                                {module.name}
                                            </AccordionTrigger>
                                            <AccordionContent className="max-w-[99%]">
                                                <div>
                                                    <p><span className="text-accent-foreground">Ширина:</span> {module.width}</p>
                                                    <p><span className="text-accent-foreground">Высота:</span> {module.height}</p>
                                                    <p><span className="text-accent-foreground">Угловой?:</span> {module.isCorner.toString()}</p>
                                                    <p><span className="text-accent-foreground">Нужна вода?:</span> {module.requiresWater.toString()}</p>
                                                    <div className="mt-2">
                                                        <KitchenModuleForm moduleId={module.id} kitchenId={kitchenId} checkAndFetchKitchen={checkAndFetchKitchen}/>
                                                    </div>
                                                </div>
                                                <Button className="m-2" onClick={triggerSubmit}>Добавить</Button>
                                            </AccordionContent>
                                        </AccordionItem>
                                    ))
                                )}
                            </ScrollArea>
                        </Accordion>
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Закрыть</AlertDialogCancel>
                    
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    );
};

export default KitchenModuleDialog;