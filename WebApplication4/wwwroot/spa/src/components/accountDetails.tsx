import { Account } from '../types/accounts';

const AccountDetails = (props: { data: Account, delete: (name: string) => void }) => {
  const deleteAccount = () => {
    props.delete(props.data.name);
  }

  return (
    <>
      <p>Account details: {props.data.name}</p>
      <p>Currency: {props.data.currency}</p>
      <p>Amount: {props.data.amount}</p>
      <button onClick={deleteAccount}>Delete me</button>
      <hr />
    </>
  );
}

export default AccountDetails;