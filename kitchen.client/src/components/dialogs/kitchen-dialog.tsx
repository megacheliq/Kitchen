import { useDialogContext } from '@/contexts/DialogContext';
import React from 'react';
import {
    AlertDialog,
    AlertDialogContent,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogCancel
} from "@/components/ui/alert-dialog";
import { ScrollArea } from '@/components/ui/scroll-area';
import { Button } from '@/components/ui/button';
import KitchenForm from '@/components/forms/kitchen-form';

const KitchenDialog: React.FC = () => {
    const { isKitchenAlertDialogOpen, setIsKitchenAlertDialogOpen, triggerSubmit } = useDialogContext(); 
    
    return (
        <AlertDialog open={isKitchenAlertDialogOpen} onOpenChange={setIsKitchenAlertDialogOpen}>
            <AlertDialogContent className="max-w-[80%]">
                <AlertDialogHeader>
                    <AlertDialogTitle>Создание кухни</AlertDialogTitle>
                    <AlertDialogDescription>
                        <ScrollArea className="h-[550px]">
                            <KitchenForm />
                        </ScrollArea>
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Закрыть</AlertDialogCancel>
                    <Button onClick={triggerSubmit}>Создать</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    );
}

export default KitchenDialog;