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
import { useDialogContext } from "@/contexts/DialogContext";
import { ScrollArea } from '@/components/ui/scroll-area';
import AddForm from '@/components/forms/add-form';
import { Button } from '@/components/ui/button';

interface AddDialogProps {
    fetchModules: () => void;
}

const AddDialog: React.FC<AddDialogProps> = ({ fetchModules }) => {
    const { isAddAlertDialogOpen, setIsAddAlertDialogOpen, triggerSubmit } = useDialogContext();

    return (
        <AlertDialog open={isAddAlertDialogOpen} onOpenChange={setIsAddAlertDialogOpen}>
            <AlertDialogContent className="max-w-[80%]">
                <AlertDialogHeader>
                    <AlertDialogTitle>Добавление модуля</AlertDialogTitle>
                    <AlertDialogDescription>
                        <ScrollArea className="h-[550px]">
                            <AddForm fetchModules={fetchModules} />
                        </ScrollArea>
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Закрыть</AlertDialogCancel>
                    <Button onClick={triggerSubmit}>Добавить</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    );
};

export default AddDialog;
