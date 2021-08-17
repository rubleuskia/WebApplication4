import { screen } from '@testing-library/react';
import { renderWithRouter } from './utils';
import App from '../App';

test('renders learn react link', () => {
  renderWithRouter(<App />);

  const accountsLink = screen.getByText("Accounts");

  expect(accountsLink).toBeInTheDocument();
  expect(accountsLink.getAttribute("href")).toEqual("/react/accounts");
});


