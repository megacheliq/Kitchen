import React, { useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import { getAllModules} from "@/services/moduleService";
import { IModule } from "@/abstract/moduleTypes";
import { useDialogContext } from "@/contexts/DialogContext";
import { createColumns } from '@/components/tables/columns';
import { DataTable } from '@/components/tables/data-table';
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import DeleteDialog from "@/components/dialogs/delete-dialog";
import ReadDialog from "@/components/dialogs/read-dialog";
import AddDialog from "@/components/dialogs/add-dialog";
import EditDialog from "@/components/dialogs/edit-dialog";
import KitchenDialog from "@/components/dialogs/kitchen-dialog";

const Hub: React.FC = () => {
    const [modules, setModules] = useState<IModule[] | []>([]);
    const [selectedModule, setSelectedModule] = useState<IModule | null>(null);
    const {
        setIsEditAlertDialogOpen,
        setIsReadAlertDialogOpen,
        setIsDeleteAlertDialogOpen,
        setIsKitchenAlertDialogOpen,
    } = useDialogContext();

    const columns = createColumns({
        onView: (module) => {
            setSelectedModule(module);
            setIsReadAlertDialogOpen(true);
        },
        onEdit: (module) => {
            setSelectedModule(module);
            setIsEditAlertDialogOpen(true);
        },
        onDelete: (module) => {
            setSelectedModule(module);
            setIsDeleteAlertDialogOpen(true);
        }
    });

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
        <>
            <div className="h-screen">
                <div className="border-b">
                    <div className="flex h-12 items-center px-4">
                        <div className="ml-auto flex items-center space-x-4">
                            <Button onClick={() => setIsKitchenAlertDialogOpen(true)}>Создать кухню</Button>
                        </div>
                    </div>
                </div>
                <div className="flex justify-center p-8">
                    <Card className='w-full h-auto'>
                        <CardHeader>
                            <CardTitle>
                                Коллекция всех модулей
                            </CardTitle>
                            <CardDescription>
                                Здесь можно изменять все модули
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <DataTable data={modules} columns={columns} />
                        </CardContent>
                    </Card>
                </div>
            </div>

            <KitchenDialog/>

            <AddDialog 
                fetchModules={fetchModules}
            />

            <EditDialog
                fetchModules={fetchModules}
                selectedModule={selectedModule}
            />

            <DeleteDialog 
                selectedModule={selectedModule}
                fetchModules={fetchModules}
            />

            <ReadDialog
                selectedModule={selectedModule}
            />
        </>
    )
}

export default Hub;