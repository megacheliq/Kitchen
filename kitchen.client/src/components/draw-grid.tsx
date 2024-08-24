import { IKitchen, IPlacedModule } from "@/abstract/kitchenTypes";
import { Group, Text as KonvaText, Rect } from 'react-konva';
import { Stage, Layer, Line, Circle } from 'react-konva';
import { useRef, useState, useEffect, useCallback } from "react";
import React from "react";

interface DrawGridProps {
    kitchen: IKitchen | null;
    cardContentRef: React.RefObject<HTMLDivElement>;
}

const DrawGrid: React.FC<DrawGridProps> = ({ kitchen, cardContentRef }) => {

    const stageRef = useRef<any>(null);
    const [scale, setScale] = useState(1);
    const [canvasSize, setCanvasSize] = useState({ width: 0, height: 0 });

    useEffect(() => {
        const updateCanvasSize = () => {
            if (cardContentRef.current) {
                const { clientWidth, clientHeight } = cardContentRef.current;
                setCanvasSize({ width: clientWidth, height: clientHeight });
            }
        };

        updateCanvasSize();

        window.addEventListener("resize", updateCanvasSize);

        return () => {
            window.removeEventListener("resize", updateCanvasSize);
        };
    }, [cardContentRef]);

    const width = kitchen?.width || 5;
    const height = kitchen?.height || 5;

    const handleWheel = (e: any) => {
        e.evt.preventDefault();
        const stage = stageRef.current;
        const oldScale = stage.scaleX();
        const pointer = stage.getPointerPosition();

        // Коэффициент изменения масштаба
        const scaleBy = 1.05;
        const newScale = e.evt.deltaY > 0 ? oldScale / scaleBy : oldScale * scaleBy;
        setScale(newScale);

        const mousePointTo = {
            x: (pointer.x - stage.x()) / oldScale,
            y: (pointer.y - stage.y()) / oldScale,
        };

        const newPos = {
            x: pointer.x - mousePointTo.x * newScale,
            y: pointer.y - mousePointTo.y * newScale,
        };

        stage.scale({ x: newScale, y: newScale });
        stage.position(newPos);
        stage.batchDraw();
    };

    const drawGrid = useCallback(() => {
        const lines = [];
        const texts = [];
        const step = 0.1;
        const textOffset = -5;

        for (let x = 0; x <= width; x += step) {
            lines.push(
                <Line
                    key={`v${x}`}
                    points={[
                        (x / width) * canvasSize.width,
                        0,
                        (x / width) * canvasSize.width,
                        canvasSize.height,
                    ]}
                    stroke="#000"
                    strokeWidth={1}
                />
            );
        }

        for (let y = 0; y <= height; y += step) {
            lines.push(
                <Line
                    key={`h${y}`}
                    points={[
                        0,
                        (y / height) * canvasSize.height,
                        canvasSize.width,
                        (y / height) * canvasSize.height,
                    ]}
                    stroke="#000"
                    strokeWidth={1}
                />
            );
        }

        for (let x = 0; x <= width; x++) {
            lines.push(
                <Line
                    key={`vBlue${x}`}
                    points={[
                        (x / width) * canvasSize.width,
                        0,
                        (x / width) * canvasSize.width,
                        canvasSize.height,
                    ]}
                    stroke="blue"
                    strokeWidth={1}
                />
            );

            texts.push(
                <KonvaText
                    key={`textX${x}`}
                    text={`${x}м`}
                    x={(x / width) * canvasSize.width + textOffset}
                    y={-15}
                    fontSize={12}
                    fill="black"
                    align="left"
                />
            );
        }

        for (let y = 0; y <= height; y++) {
            lines.push(
                <Line
                    key={`hBlue${y}`}
                    points={[
                        0,
                        (y / height) * canvasSize.height,
                        canvasSize.width,
                        (y / height) * canvasSize.height,
                    ]}
                    stroke="blue"
                    strokeWidth={1}
                />
            );

            texts.push(
                <KonvaText
                    key={`textY${y}`}
                    text={`${y}м`}
                    x={-20}
                    y={(y / height) * canvasSize.height - -textOffset}
                    fontSize={12}
                    fill="black"
                    align="right"
                />
            );
        }

        return (
            <>
                {lines}
                {texts}
            </>
        )
    }, [kitchen, canvasSize]);

    const drawModules = useCallback(() => {
        if (!kitchen) return null;
    
        return kitchen.modules.map((placedModule: IPlacedModule) => {
            const module = placedModule.module;
            const coord = placedModule.coordinate;
    
            // Рассчитываем размеры прямоугольника в зависимости от ориентации модуля
            const rectWidth = module.width / width * canvasSize.width;
            const rectHeight = module.height / height * canvasSize.height;
    
            // Рассчитываем координаты центра прямоугольника для отображения текста
            const rectX = (coord.x / width) * canvasSize.width;
            const rectY = (coord.y / height) * canvasSize.height;
    
            // Определяем угол поворота
            const rotation = 0;
    
            // Формируем дополнительные свойства для отображения
            const additionalInfo = [];
            if (module.isCorner) additionalInfo.push("Угловой");
            if (module.requiresWater) additionalInfo.push("Нужна вода");
            const additionalText = additionalInfo.join(", ");
    
            return (
                <Group key={module.id} x={rectX} y={rectY} rotation={rotation}>
                    <Rect
                        x={0} 
                        y={0}
                        width={rectWidth}
                        height={rectHeight}
                        fill="green"
                        stroke="black"
                        strokeWidth={1}
                    />
                    <KonvaText
                        text={module.name}
                        x={rectWidth / 2}
                        y={rectHeight / 2 - 10}
                        fontSize={14}
                        fill="black"
                        align="center"
                        verticalAlign="middle"
                        offsetX={module.name.length * 3.5}
                    />
                    {additionalText && (
                        <KonvaText
                            text={additionalText}
                            x={rectWidth / 2}
                            y={rectHeight / 2 + 10}
                            fontSize={14}
                            fill="black"
                            align="center"
                            verticalAlign="middle"
                            offsetX={additionalText.length * 3.5}
                        />
                    )}
                </Group>
            );
        });
    }, [kitchen, width, height, canvasSize]);
    



    return (
        <Stage
            className="mr-2"
            ref={stageRef}
            width={canvasSize.width}
            height={canvasSize.height}
            scaleX={scale}
            scaleY={scale}
            onWheel={handleWheel}
            draggable
        >
            <Layer>
                {drawGrid()}
                {kitchen && kitchen.waterPipe && (
                    <Circle
                        x={(kitchen.waterPipe.x / width) * canvasSize.width}
                        y={(kitchen.waterPipe.y / height) * canvasSize.height}
                        radius={5}
                        fill="red"
                        stroke="black"
                        strokeWidth={1}
                    />
                )}
                {drawModules()}
            </Layer>
        </Stage>
    );
};

export default DrawGrid;