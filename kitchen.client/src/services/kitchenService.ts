import { IAddModuleDto, ICommandDto } from '@/abstract/kitchenTypes';
import axiosClient from '@/axios-client';
import { toast } from "sonner"

export const getAllKitchens = async () => {
    try {
        const response = await axiosClient.get('Kitchen/GetAll')
        return response.data;
    } catch (error: any) {
        console.error('Failed to get kitchens:', error);
        toast.error(error.response?.data?.message ||'Не удалось получить кухни');
    }
}

export const getKitchenById = async (id: string) => {
    try {
        const response = await axiosClient.get(`Kitchen/Get/${id}`);
        return response.data;
    } catch (error: any) {
        console.error('Failed to get kitchen:', error);
        toast.error(error.response?.data?.message ||'Не удалось получить кухню');
    }
}

export const addModuleToKitchen = async (commandDto: IAddModuleDto) => {
    try {
        const response = await axiosClient.post('Kitchen/AddModule', {
            kitchenId: commandDto.kitchenId,
            moduleId: commandDto.moduleId,
            coordinate: commandDto.coordinate,
            orientation: commandDto.orientation,
        });
        toast.success('Модуль успешно добавлен в кухню');
        return response.data;
    } catch (error: any) {
        console.error('Failed to add module to kitchen:', error);
        toast.error(error.response?.data?.message ||'Не удалось добавить модуль в кухню');
    }
}

export const createKitchen = async (commandDto: ICommandDto) => {
    try {
        const response = await axiosClient.post('Kitchen/Create', {
            commandDto: commandDto
        });
        toast.success('Кухня успешно создана');
        return response.data;
    } catch (error: any) {
        console.error('Failed to add kitchen:', error);
        toast.error(error.response?.data?.message || 'Не удалось создать кухню');
    }
}

export const updateKitchen = async (commandDto: ICommandDto, id: string) => {
    try {
        await axiosClient.put(`Kitchen/Update/${id}`, {
            width: commandDto.width,
            height: commandDto.height,
            waterPipe: commandDto.waterPipe,
            modules: commandDto.modules
        });
        toast.success('Кухня успешно обновлена');
        return true;
    } catch (error: any) {
        console.error('Failed to update kitchen:', error);
        toast.error(error.response?.data?.message ||'Не удалось обновить кухню');
    }
}

export const deleteKitchen = async (id: string) => {
    try {
        await axiosClient.delete(`Kitchen/Delete/${id}`);
        toast.success('Кухня успешно удалена');
        return true;
    } catch (error: any) {
        console.error('Failed to delete kitchen:', error);
        toast.error(error.response?.data?.message ||'Не удалось удалить кухню');
    }
}