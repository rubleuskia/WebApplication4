import { Button } from 'react-bootstrap';

interface Props {
    heading: string;
    src: string;
}

const Card = (props: Props) => {
    const { heading, src } = props;

    return (
        <div className="card-content">
            <div className="card-img">
                <img src={src} alt="" />
                <span><h4>{heading}</h4></span>
            </div>
            <div className="card-desc">
                <h3>Heading</h3>
                <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Laboriosam, voluptatum! Dolor quo, perspiciatis
                    voluptas totam</p>
                <a href="#" className="btn-card">Read</a>
            </div>
        </div>
    );
}

