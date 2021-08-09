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
          Edit <code>src/App.tsx</code> and save to reload (x).
        </p>
        <Accounts />
      </header>
    </div>
  );
}

export default App;
