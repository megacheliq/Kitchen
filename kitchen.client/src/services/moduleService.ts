import { ICommandDto } from '@/abstract/moduleTypes';
import axiosClient from '@/axios-client';
import { toast } from "sonner"

export const getAllModules = async () => {
    try {
        const response = await axiosClient.get('Module/GetAll')
        return response.data;
    } catch (error: any) {
        console.error('Failed to get modules:', error);
        toast.error(error.response?.data?.message || 'Не удалось получить модули');
    }
}

export const getModuleById = async (id: string) => {
    try {
        const response = await axiosClient.get(`Module/Get/${id}`);
        return response.data;
    } catch (error: any) {
        console.error('Failed to get module:', error);
        toast.error(error.response?.data?.message || 'Не удалось получить модуль');
    }
}

export const createModule = async (commandDto: ICommandDto) => {
    try {
        await axiosClient.post('Module/Create', {
            commandDto: commandDto
        });
        toast.success('Модуль успешно добавлен');
        return true;
    } catch (error: any) {
        console.error('Failed to add module:', error);
        toast.error(error.response?.data?.message || 'Не удалось добавить модуль');
    }
}

export const updateModule = async (commandDto: ICommandDto, id: string) => {
    try {
        await axiosClient.put(`Module/Update/${id}`, {
            name: commandDto.name,
            width: commandDto.width,
            height: commandDto.height,
            isCorner: commandDto.isCorner,
            requiresWater: commandDto.requiresWater
        });
        toast.success('Модуль успешно обновлен');
        return true;
    } catch (error: any) {
        console.error('Failed to update module:', error);
        toast.error(error.response?.data?.message || 'Не удалось обновить модуль');
    }
}

export const deleteModule = async (id: string) => {
    try {
        await axiosClient.delete(`Module/Delete/${id}`);
        toast.success('Модуль успешно удален');
        return true;
    } catch (error: any) {
        console.error('Failed to delete module:', error);
        toast.error(error.response?.data?.message || 'Не удалось удалить модуль');
    }
}