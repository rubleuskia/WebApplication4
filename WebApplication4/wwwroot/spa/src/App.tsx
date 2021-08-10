import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Accounts from './components/accounts/list';

const App = () => {
  return (
    <>
      <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
          <div className="container">
            <a className="navbar-brand" href="/" >WebApplication4</a>
            <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
              aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
              <ul className="navbar-nav flex-grow-1">
                <li className="nav-item">
                  <a className="nav-link text-dark" href="/" >Home</a>
                </li>
                <li className="nav-item">
                  <a className="nav-link text-dark" href="/" >Privacy</a>
                </li>
                <li className="nav-item">
                  <a className="nav-link text-dark" href="/spa">SPA</a>
                </li>
                <li className="nav-item">
                  <a className="nav-link text-dark" href="/">Accounts</a>
                </li>
              </ul>
            </div>
          </div>
        </nav>
      </header>
      <div className="container">
        <main role="main" className="pb-3">
          <Accounts />
        </main>
      </div>
    </>
  );
}

export default App;
