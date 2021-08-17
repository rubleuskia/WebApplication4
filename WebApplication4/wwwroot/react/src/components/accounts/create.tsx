import React, { useState } from "react";
import { Alert, Button, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { createAccount } from "../../services/accountService";

const Create = () => {
    const [amount, setAmount] = useState(0);
    const [currency, setCurrency] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleCurrencyChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setCurrency(e.target.value);
    }

    const handleAmountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setAmount(+e.target.value);
    }

    const onSave = () => {
        createAccount(amount, currency).then(() => {
            navigate(`/react/accounts`);
        }).catch((e) => {
            setError(e.message);
        });
    }

    return (
        <>
            {error && <Alert variant='danger'>Failed to create account.</Alert>}
            <Form>
                <Form.Label as="legend" column sm={2}>
                    Create account
                </Form.Label>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Initial amount</Form.Label>
                    <Form.Control type="number" placeholder="Enter amount" onChange={handleAmountChange} value={amount} />
                    <Form.Text className="text-muted">
                        initial amount to acquire to your account
                    </Form.Text>
                </Form.Group>

                <Form.Select className="mb-3" aria-label="Default select example" onChange={handleCurrencyChange} value={currency}>
                    <option>Select currency: </option>
                    <option value="">Please select currency</option>
                    <option value="USD">US dollar</option>
                    <option value="RUB">Russian ruble</option>
                    <option value="BYN">Belorussian ruble</option>
                </Form.Select>
                <Button variant="primary" onClick={onSave}>
                    Save
                </Button>
            </Form>
        </>
    );
}

export default Create;