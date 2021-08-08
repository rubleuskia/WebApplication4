import React, { MouseEventHandler, useState } from 'react';
import LearnReactLink from './learnReactLink';

const Links = (props: { links: { text: string, link: string }[] }) => {
    let [count, setCount] = useState(0);

    const handleClick = () => {
        setCount(++count);
    };

    return (
        <>
            {props.links.map(link =>
                <LearnReactLink
                    text={link.text}
                    link={link.link}
                    handleClick={handleClick}
                    count={count}
                />
            )}
        </>
    );
}

export default Links;