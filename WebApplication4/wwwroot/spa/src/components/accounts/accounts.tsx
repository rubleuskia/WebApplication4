import { useEffect, useState } from "react";
import { Alert } from "react-bootstrap";
import { fetchAccounts } from "../../services/accountService";
import { AccountDto } from "../../types/accounts";
import Account from "./account";
import Header from "./header";

const Accounts = () => {
    const [accounts, setAccounts] = useState<AccountDto[]>([]);
    const [error, setError] = useState("");

    useEffect(() => {
        fetchAccounts()
            .then((accounts) => {
                setAccounts(accounts);
                setError("");
            })
            .catch((e: Error) => {
                setError(e.message);
            });
    }, []);

    const onDelete = (id: string) => {
        const filtered = accounts.filter(x => x.id !== id);
        setAccounts(filtered);
    }

    return (
        <>
            <Header />
            <div className="row">
                {error && <Alert variant='danger'>Failed to fetch.</Alert>}
                {!error && accounts.length === 0 && <Alert variant='warning'>No accounts yet.</Alert>}
                {accounts.length > 0 && accounts.map((acc) =>
                    <Account key={acc.id} account={acc} onDelete={onDelete} />)}
            </div>
        </>
    );
}

export default Accounts;