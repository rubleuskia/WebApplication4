import { Account } from '../types/accounts';

const AccountDetails = (props: { data: Account, delete: (name: string) => void }) => {
  const deleteAccount = () => {
    props.delete(props.data.id);
  }

  return (
    <>
      <hr />
      <p>Account details: {props.data.id}</p>
      <p>Currency: {props.data.currencyCharCode}</p>
      <p>Amount: {props.data.amount}</p>
      <p>Version: {props.data.rowVersion}</p>
      <p>Updated at: {props.data.updatedAt}</p>
      <p>Created at: {props.data.createdAt}</p>
      <button onClick={deleteAccount}>Delete me</button>
      <hr />
    </>
  );
}

export default AccountDetails;