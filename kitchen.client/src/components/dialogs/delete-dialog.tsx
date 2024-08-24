import React, { useEffect, useState } from 'react';
import {
    AlertDialog,
    AlertDialogContent,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogCancel,
    AlertDialogAction
} from "@/components/ui/alert-dialog";
import { useDialogContext } from "@/contexts/DialogContext";
import { LoadingSpinner } from "@/components/ui/loading-spinner";
import { IModule } from '@/abstract/moduleTypes';
import { deleteModule } from '@/services/moduleService';

interface DeleteDialogProps {
    selectedModule: IModule | null;
    fetchModules: () => void;
}

const DeleteDialog: React.FC<DeleteDialogProps> = ({ selectedModule, fetchModules }) => {
    const { isDeleteAlertDialogOpen, setIsDeleteAlertDialogOpen } = useDialogContext();
    const [deleted, setDeleted] = useState<boolean>(false);

    useEffect(() => {
        fetchModules();
    }, [deleted])

    return (
        <AlertDialog open={isDeleteAlertDialogOpen} onOpenChange={setIsDeleteAlertDialogOpen}>
            <AlertDialogContent>
                {selectedModule ? (
                    <div>
                        <AlertDialogHeader>
                            <AlertDialogTitle>Вы действительно хотите удалить модуль?</AlertDialogTitle>
                            <AlertDialogDescription>
                                Удаленный модуль нельзя будет вернуть
                            </AlertDialogDescription>
                        </AlertDialogHeader>
                        <AlertDialogFooter className='mt-8'>
                            <AlertDialogCancel>Отменить</AlertDialogCancel>
                            <AlertDialogAction onClick={() => {
                                    deleteModule(selectedModule.id);
                                    setDeleted(true);
                                }}>
                                Удалить
                                </AlertDialogAction>
                        </AlertDialogFooter>
                    </div>
                ) : <LoadingSpinner />}
            </AlertDialogContent>
        </AlertDialog>
    );
};

export default DeleteDialog;