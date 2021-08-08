import React from 'react';
import logo from './logo.svg';
import './App.css';
import Accounts from './components/accounts';
import { Currency } from './types/accounts';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload (modified177111).
        </p>
        <Accounts accounts={[
          { name: "Account 1", currency: Currency.BYN, amount: 1000 },
          { name: "Account 2", currency: Currency.USD, amount: 5000 },
          { name: "Account 3", currency: Currency.EUR, amount: 7000 },
        ]} />
      </header>
    </div>
  );
}

export default App;
