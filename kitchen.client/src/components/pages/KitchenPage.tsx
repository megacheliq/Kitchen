import { IKitchen } from "@/abstract/kitchenTypes";
import { getKitchenById } from "@/services/kitchenService";
import { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button } from "@/components/ui/button";
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import DrawGrid from "@/components/draw-grid";
import { useDialogContext } from "@/contexts/DialogContext";
import KitchenModuleDialog from "../dialogs/kitchen-module-dialog";
import {
    HoverCard,
    HoverCardContent,
    HoverCardTrigger,
} from "@/components/ui/hover-card"
import { Info } from "lucide-react";

const KitchenPage: React.FC = () => {
    const { kitchenId } = useParams<{ kitchenId: string }>();
    const navigate = useNavigate();
    const [kitchen, setKitchen] = useState<IKitchen | null>(null);
    const cardContentRef = useRef<HTMLDivElement>(null);
    const { setIsModuleToKitchenAlertDialogOpen } = useDialogContext();

    const checkAndFetchKitchen = async (kitchenId: string) => {
        try {
            const fetchedKitchen = await getKitchenById(kitchenId);
            setKitchen(fetchedKitchen);
        } catch (error) {
            navigate('/404');
        }
    }

    useEffect(() => {
        checkAndFetchKitchen(kitchenId || "");
    }, [kitchenId]);

    return (
        <>
            <div className="h-screen flex flex-col">
                <div className="border-b">
                    <div className="flex h-12 items-center px-4">
                        <div className="ml-auto flex items-center space-x-4">
                            <Button onClick={() => setIsModuleToKitchenAlertDialogOpen(true)}>
                                Добавить модуль
                            </Button>
                        </div>
                    </div>
                </div>
                <div className="flex justify-center p-8 flex-grow">
                    <Card className="w-full flex flex-col">
                        <CardHeader>
                            <div className="flex gap-4">
                                <div>
                                    <CardTitle>Кухня</CardTitle>
                                    <CardDescription>
                                        Размер: {kitchen?.width}м. на {kitchen?.height}м.
                                    </CardDescription>
                                </div>
                                <div className="mt-2">
                                    <CardTitle>
                                        <HoverCard>
                                            <HoverCardTrigger><Info className="cursor-pointer" /></HoverCardTrigger>
                                            <HoverCardContent>
                                                <CardDescription>
                                                    Добавление модулей происходит 
                                                    внутри модального окна, которое
                                                    открывается по кнопке Добавить модуль.
                                                    Указываемые координаты являются левым верхним углом модуля
                                                </CardDescription>
                                            </HoverCardContent>
                                        </HoverCard>
                                    </CardTitle>
                                </div>
                            </div>

                        </CardHeader>
                        <CardContent
                            className="flex-grow overflow-auto"
                            ref={cardContentRef}
                        >
                            <DrawGrid kitchen={kitchen} cardContentRef={cardContentRef} />
                        </CardContent>
                    </Card>
                </div>
            </div>

            <KitchenModuleDialog checkAndFetchKitchen={checkAndFetchKitchen} kitchenId={kitchenId || ""} />
        </>


    );
}

export default KitchenPage;