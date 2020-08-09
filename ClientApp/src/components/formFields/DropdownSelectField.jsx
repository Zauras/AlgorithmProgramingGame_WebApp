import React from 'react';
import Select from 'react-select';

import { FormField } from './FormField';

export const DropdownSelectField = ({ name, options, ...restProps }) => {
    return (
        <FormField name={name}>
            <Select options={options} {...restProps} />
        </FormField>
    );
};
