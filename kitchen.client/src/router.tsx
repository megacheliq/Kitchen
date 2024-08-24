import { createBrowserRouter, Navigate } from "react-router-dom";
import Hub from "./components/pages/Hub";
import { DialogProvider } from "@/contexts/DialogContext";
import NotFound from "@/components/pages/NotFound";
import KitchenPage from "./components/pages/KitchenPage";

const router = createBrowserRouter([
    {
        path: '/',
        element: <DialogProvider><Hub /></DialogProvider>
    },
    {
        path: '/kitchen/:kitchenId',
        element: <DialogProvider><KitchenPage/></DialogProvider>
    },
    {
        path: '/404',
        element: <NotFound />
    },
    {
        path: '*',
        element: <Navigate to='/404' />
    },
])

export default router;