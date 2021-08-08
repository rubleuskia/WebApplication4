import React from 'react';
import logo from './logo.svg';
import './App.css';
import Links from './components/links';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload (modified177111).
        </p>
        <Links links={[
          { text: "Learn React", link: "https://reactjs.org" },
          { text: "Learn React Copy", link: "https://reactjs.org" },
          { text: "Learn React Copy Copy", link: "https://reactjs.org" },
        ]} />
      </header>
    </div>
  );
}

export default App;
