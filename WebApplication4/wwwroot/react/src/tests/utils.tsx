import { MemoryRouter } from 'react-router-dom';
import { render } from '@testing-library/react';

export const renderWithRouter = (children: JSX.Element) =>
    render(<MemoryRouter>{children}</MemoryRouter>);
