import { useDialogContext } from "@/contexts/DialogContext";
import React from "react";
import {
    AlertDialog,
    AlertDialogContent,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogCancel
} from "@/components/ui/alert-dialog";
import { Button } from '@/components/ui/button';
import { optimalPlace } from "@/services/kitchenService";

interface OptimalPlaceDialogProps {
    checkAndFetchKitchen: (kitchenId: string) => Promise<void>;
    kitchenId: string;
}

const OptimalPlaceDialog: React.FC<OptimalPlaceDialogProps> = ({ checkAndFetchKitchen, kitchenId }) => {
    const { isOptimalPlaceAlerDialogOpen, setIsOptimalPlaceAlertDialogOpen } = useDialogContext();

    const onSubmit = async () => {
        const response = await optimalPlace(kitchenId);
        if (response) {
            checkAndFetchKitchen(kitchenId);
        }
        setIsOptimalPlaceAlertDialogOpen(false);
    }

    return (
        <AlertDialog open={isOptimalPlaceAlerDialogOpen} onOpenChange={setIsOptimalPlaceAlertDialogOpen}>
            <AlertDialogContent className="max-w-[80%]">
                <AlertDialogHeader>
                    <AlertDialogTitle>Оптимально размещение модулей</AlertDialogTitle>
                    <AlertDialogDescription>
                        Это оптимально разместит заранее подготовленные модули<br />
                        Модули:<br/>
                        Плита - ширина: 3, длина: 1, не в углу, не нужна вода;<br/>
                        Раковина - ширина: 2, высота: 1, не в углу, нужна вода.
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Закрыть</AlertDialogCancel>
                    <Button onClick={() => onSubmit()}>Разместить</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    );
};

export default OptimalPlaceDialog;