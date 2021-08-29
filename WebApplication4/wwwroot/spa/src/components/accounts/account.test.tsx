import React from 'react';
import { fireEvent, render, screen } from '@testing-library/react';
import Account from './account';
import { AccountDto } from '../../types/accounts';
import { MemoryRouter } from 'react-router-dom';

test('renders account data', () => {
    // arrange
    const account: AccountDto = { amount: 100, currencyName: "US dollar", id: "100500" };

    // act = render
    renderWithRouter(<Account account={account} onDelete={() => { }} />);

    // assert
    expect(screen.getByText("Account (100500)")).toBeInTheDocument();
    expect(screen.getByText("US dollar")).toBeInTheDocument();
    expect(screen.getByTestId("amount-field")).toHaveTextContent("100");
});

test('to delete card when button clicked', async () => {
    // arrange
    const account: AccountDto = { amount: 100, currencyName: "US dollar", id: "100500" };

    const onDeleteSpy = jest.fn();
    renderWithRouter(<Account account={account} onDelete={onDeleteSpy} />);

    // act
    const button = await screen.findByText("Delete");
    fireEvent.click(button);

    // assert
    expect(onDeleteSpy).toHaveBeenCalledTimes(1);
    expect(onDeleteSpy).toHaveBeenCalledWith("100500");
});

const renderWithRouter = (children: JSX.Element) => render(<MemoryRouter>{children}</MemoryRouter>);