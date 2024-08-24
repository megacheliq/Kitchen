import { IModule } from "@/abstract/moduleTypes";
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
import { LoadingSpinner } from "@/components/ui/loading-spinner";
import { ScrollArea } from '@/components/ui/scroll-area';

interface ReadDialogProps {
    selectedModule: IModule | null;
}

const ReadDialog: React.FC<ReadDialogProps> = ({ selectedModule }) => {
    const { isReadAlertDialogOpen, setIsReadAlertDialogOpen } = useDialogContext();

    return (
        <AlertDialog open={isReadAlertDialogOpen} onOpenChange={setIsReadAlertDialogOpen}>
            <AlertDialogContent className="max-w-[75%]">
                <AlertDialogHeader>
                    <AlertDialogTitle>Детали модуля</AlertDialogTitle>
                    <AlertDialogDescription>
                        {selectedModule ? (
                                <ScrollArea className="h-[400px]">
                                        <div className='flex gap-4'>
                                            <div className='w-1/2'>
                                                <p><span className="text-accent-foreground">Название:</span> {selectedModule.name}</p>
                                                <p><span className="text-accent-foreground">Ширина:</span> {selectedModule.width}</p>
                                                <p><span className="text-accent-foreground">Высота:</span> {selectedModule.height}</p>
                                                <p><span className="text-accent-foreground">Угловой:</span> {selectedModule.isCorner.toString()}</p>
                                                <p><span className="text-accent-foreground">Нужна вода:</span> {selectedModule.requiresWater.toString()}</p>
                                            </div>
                                        </div>
                                </ScrollArea>
                        ) : <LoadingSpinner />}
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Закрыть</AlertDialogCancel>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    )
}

export default ReadDialog;