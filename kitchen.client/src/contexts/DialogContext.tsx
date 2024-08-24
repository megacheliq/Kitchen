import React, { createContext, useContext, useRef, useState, ReactNode } from 'react';

interface DialogContextType {
    isEditAlertDialogOpen: boolean;
    setIsEditAlertDialogOpen: (open: boolean) => void;
    isAddAlertDialogOpen: boolean;
    setIsAddAlertDialogOpen: (open: boolean) => void;
    isDeleteAlertDialogOpen: boolean;
    setIsDeleteAlertDialogOpen: (open: boolean) => void;
    isReadAlertDialogOpen: boolean;
    setIsReadAlertDialogOpen: (open: boolean) => void;
    isKitchenAlertDialogOpen: boolean;
    setIsKitchenAlertDialogOpen: (open: boolean) => void;
    isModuleToKitchenAlertDialogOpen: boolean;
    setIsModuleToKitchenAlertDialogOpen: (open: boolean) => void;
    triggerSubmit: () => void;
    formRef: React.RefObject<HTMLFormElement>;
}

const DialogContext = createContext<DialogContextType | null>(null);

export const useDialogContext = (): DialogContextType => {
    const context = useContext(DialogContext);
    if (!context) {
        throw new Error('useDialogContext must be used within a DialogProvider');
    }
    return context;
};

interface DialogProviderProps {
    children: ReactNode;
}

export const DialogProvider: React.FC<DialogProviderProps> = ({ children }) => {
    const [isEditAlertDialogOpen, setIsEditAlertDialogOpen] = useState(false);
    const [isAddAlertDialogOpen, setIsAddAlertDialogOpen] = useState(false);
    const [isReadAlertDialogOpen, setIsReadAlertDialogOpen] = useState(false);
    const [isDeleteAlertDialogOpen, setIsDeleteAlertDialogOpen] = useState(false);
    const [isKitchenAlertDialogOpen, setIsKitchenAlertDialogOpen] = useState(false);
    const [isModuleToKitchenAlertDialogOpen, setIsModuleToKitchenAlertDialogOpen] = useState(false);
    const formRef = useRef<HTMLFormElement>(null);

    const triggerSubmit = () => {
        formRef.current?.requestSubmit();
    };

    return (
        <DialogContext.Provider value={{
            isEditAlertDialogOpen,
            setIsEditAlertDialogOpen,
            isAddAlertDialogOpen,
            setIsAddAlertDialogOpen,
            isReadAlertDialogOpen,
            setIsReadAlertDialogOpen,
            isDeleteAlertDialogOpen,
            setIsDeleteAlertDialogOpen,
            isKitchenAlertDialogOpen,
            setIsKitchenAlertDialogOpen,
            isModuleToKitchenAlertDialogOpen,
            setIsModuleToKitchenAlertDialogOpen,
            triggerSubmit,
            formRef,
        }}>
            {children}
        </DialogContext.Provider>
    );
};
