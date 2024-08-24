import { IModule } from "@/abstract/moduleTypes";

export interface IKitchen {
    id: string;
    width: number;
    height: number;
    waterPipe: ICoordinate;
    modules: IPlacedModule[];
}

export interface ICoordinate {
    x: number;
    y: number;
}

export interface ICommandDto {
    width: number;
    height: number;
    waterPipe: ICoordinate;
    modules: IPlacedModule[];
}

export interface IAddModuleDto {
    kitchenId: string;
    moduleId: string;
    coordinate: ICoordinate;
    orientation: number;
}

export interface IPlacedModule {
    module: IModule;
    coordinate: ICoordinate;
    orientation: number;
}