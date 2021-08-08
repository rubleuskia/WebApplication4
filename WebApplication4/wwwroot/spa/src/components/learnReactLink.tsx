import React, { MouseEventHandler, useState } from 'react';

const LearnReactLink = (props: {
  text: string,
  link: string,
  count: number;
  handleClick: () => void
}) => {
  return (
    <>
      <p>Count: {props.count}</p>
      <button onClick={props.handleClick}>Click me!</button>
      <a
        className="App-link"
        href={props.link}
        target="_blank"
        rel="noopener noreferrer"
      >
        {props.text}
      </a>
    </>
  );
}

export default LearnReactLink;