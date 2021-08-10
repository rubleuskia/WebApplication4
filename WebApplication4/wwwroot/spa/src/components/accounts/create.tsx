import { useState } from 'react';
import { Button, Form } from 'react-bootstrap';

interface Props {
    onSave: (amount: number, currencyCode: string) => void;
}

const Create = ({ onSave }: Props) => {
    const [amount, setAmount] = useState(0);
    const [currency, setCurrency] = useState("");

    const handleAmountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAmount(+event.target.value);
    };

    const handleCurrencyChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setCurrency(event.target.value);
    };

    const handleSave = () => {
        onSave(amount, currency);
    };

    return (
        <Form>
            <Form.Group className="mb-3" controlId="formBasicEmail">
                <Form.Label>Amount</Form.Label>
                <Form.Control type="number" placeholder="Enter amount" onChange={handleAmountChange} />
                <Form.Text className="text-muted" >
                    Initial amount to acquire to your account.
                </Form.Text>
            </Form.Group>

            <Form.Select className="mb-3" aria-label="Default select example" onChange={handleCurrencyChange}>
                <option>Select currency: </option>
                <option value="USD">US dollar</option>
                <option value="RUB">Russian ruble</option>
                <option value="BYN">Belorussian ruble</option>
            </Form.Select>
            <Button variant="primary" onClick={handleSave}>
                Save
            </Button>
        </Form>
    );
}

export default Create;