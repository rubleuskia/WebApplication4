import Accounts from "./components/accounts/accounts";
import Acquire from "./components/accounts/acquire";
import Create from "./components/accounts/create";
import Layout from "./components/accounts/layout";

export const routes = [
    {
        path: '/spa/accounts',
        element: <Layout />,
        children: [
            { path: '/', element: <Accounts /> },
            { path: 'create', element: <Create /> },
            { path: 'acquire/:accountId', element: <Acquire /> },
            // { path: 'withdraw/:accountId', element: <Withdraw /> },
        ]
    },
];