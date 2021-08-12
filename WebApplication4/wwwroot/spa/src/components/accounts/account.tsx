import { Button, Card } from "react-bootstrap";
import { AccountDto } from "../../types/accounts";
import logo from './../../logo.svg';

interface Props {
    account: AccountDto;
    onDelete: (id: string) => void;
}

const Account = (props: Props) => {
    const { account, onDelete } = props;
    return (
        <>
            <Card style={{ width: '18rem' }} className="m-2">
                <Card.Img variant="top" src={logo} />
                <Card.Body>
                    <Card.Title>Account ({account.id})</Card.Title>
                    <Card.Text>
                        <h5 className="card-title">{account.amount}</h5>
                        {account.currencyName}
                    </Card.Text>
                    <Button variant="primary m-1">Acquire</Button>
                    <Button variant="primary m-1">Withdraw</Button>
                    <Button variant="primary m-1" onClick={() => onDelete(account.id)}>Delete</Button>
                </Card.Body>
            </Card>
        </>
    );
}

export default Account;