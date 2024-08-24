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
import EditForm from '@/components/forms/edit-form';
import { IModule } from '@/abstract/moduleTypes';
import { Button } from '@/components/ui/button';

interface EditDialogProps {
    fetchModules: () => void;
    selectedModule: IModule | null;
}

const EditDialog: React.FC<EditDialogProps> = ({ fetchModules, selectedModule }) => {
    const { isEditAlertDialogOpen, setIsEditAlertDialogOpen, triggerSubmit } = useDialogContext();

    return (
        <AlertDialog open={isEditAlertDialogOpen} onOpenChange={setIsEditAlertDialogOpen}>
            <AlertDialogContent className="max-w-[80%]">
                <AlertDialogHeader>
                    <AlertDialogTitle>Редактирование модуля</AlertDialogTitle>
                    <AlertDialogDescription>
                        <ScrollArea className="h-[550px]">
                            <EditForm fetchModules={fetchModules} selectedModule={selectedModule}/>
                        </ScrollArea>
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Закрыть</AlertDialogCancel>
                    <Button onClick={triggerSubmit}>Обновить</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    );
};

export default EditDialog;
