import Accounts from "./components/accounts/accounts";
import Acquire from "./components/accounts/acquire";
import Create from "./components/accounts/create";
import Layout from "./components/accounts/layout";
import MainPage from "./components/mainPage";

export const routes = [
    {
        path: '/react/accounts',
        element: <Layout />,
        children: [
            { path: '/', element: <Accounts /> },
            { path: 'create', element: <Create /> },
            { path: 'acquire/:accountId', element: <Acquire /> },
            // { path: 'withdraw/:accountId', element: <Withdraw /> },
        ]
    },
    {
        path: '/react',
        element: <MainPage />,
    },
];