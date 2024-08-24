export interface IModule {
    id: string;
    name: string;
    width: number;
    height: number;
    isCorner: boolean;
    requiresWater: boolean;
}

export interface ICommandDto {
    name: string;
    width: number;
    height: number;
    isCorner: boolean;
    requiresWater: boolean;
}