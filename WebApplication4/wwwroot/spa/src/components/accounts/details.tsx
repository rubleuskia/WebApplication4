import { Button, Card } from 'react-bootstrap';
import logo from './../../logo.svg';
import { Account } from '../../types/accounts';

interface Props {
    account: Account;
    remove: (id: string) => void;
}

const Details = ({ account, remove }: Props) => {
    return (
        <Card style={{ width: '18rem' }}>
            <Card.Img variant="top" src={logo} />
            <Card.Body>
                <Card.Title>Account ({account.id})</Card.Title>
                <Card.Text>
                    <h5 className="card-title">{account.amount}</h5>
                    <p className="card-text">{account.currencyCharCode}</p>
                </Card.Text>
                <Button onClick={() => remove(account.id)} variant="primary">Delete</Button>
            </Card.Body>
        </Card>
    );
}

export default Details;