import React from 'react';
import Select from 'react-select';

export const DropdownSelectField = ({ name, options, ...restProps }) => {
    return (
        <div>
            <span>{name}</span>
            <Select options={options} {...restProps} />
        </div>
    );
};
