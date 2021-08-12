import { Button } from 'react-bootstrap';

interface Props {
    text: string;
    href: string;
    object?: { x: number, y: number };
    func?: (text: string) => void;
}

const Link = (props: Props) => {
    const { text, href, object, func } = props;

    if (func) {
        func(text);
    }

    return (
        <Button onClick={() => console.log("Clicked")} className="m-3">
            {text}
        </Button>
    );
}

export default Link; // private