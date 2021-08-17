import { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useLocation, useParams } from "react-router-dom";
import { AccountDto } from "../../types/accounts";

const Acquire = () => {
    const [amount, setAmount] = useState(0);

    let { accountId } = useParams();
    const location = useLocation();
    const account = location.state as AccountDto;

    const handleAmountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAmount(+event.target.value);
    };

    const onAcquire = () => {
        // state
    }

    return (
        <>
            <Form>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Current amount ({accountId})</Form.Label>
                    <Form.Control type="number" disabled={true} value={account.amount} />
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Amount to acquire</Form.Label>
                    <Form.Control type="number" placeholder="Enter amount" value={amount} onChange={handleAmountChange} />
                    <Form.Text className="text-muted" >
                        Initial amount to acquire to your account.
                    </Form.Text>
                </Form.Group>
                <Button variant="primary" onClick={onAcquire}>
                    Acquire
                </Button>
            </Form>
        </>
    );
}

export default Acquire;