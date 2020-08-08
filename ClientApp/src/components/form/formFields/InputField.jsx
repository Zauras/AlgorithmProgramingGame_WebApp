import React from 'react';

export const InputField = ({ name, type = 'text', ...restProps }) => (
    <div>
        <span>{name}</span>
        <input type={type} {...restProps} />
    </div>
);
