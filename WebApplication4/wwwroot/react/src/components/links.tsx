import React from "react";
import Link from "./link";

const Links = () => {
    const run = (text: string) => {
        console.log(text);
    }

    return (
        <>
            <Link text="Learn React" href="https://reactjs.org" object={{ x: 1, y: 1 }} func={run} />
            <Link text="Learn TypeScript" href="https://www.typescriptlang.org/" />
            <Link text="Learn TypeScript" href="https://www.typescriptlang.org/" func={run} />
        </>
    );
}


class LinksClassComponent extends React.Component {
    private run = (text: string) => {
        console.log(text);
    }

    render() {
        return (
            <>
                <Link text="Learn React" href="https://reactjs.org" object={{ x: 1, y: 1 }} func={this.run} />
                <Link text="Learn TypeScript" href="https://www.typescriptlang.org/" />
                <Link text="Learn TypeScript" href="https://www.typescriptlang.org/" func={this.run} />
            </>
        );
    }
}


export default LinksClassComponent;