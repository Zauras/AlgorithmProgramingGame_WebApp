import React, { Fragment, useReducer } from 'react';
import { InputField } from '../../components/form/formFields/InputField';
import { DropdownSelectField } from '../../components/form/formFields/DropdownSelectField';
import { TextField } from '../../components/form/formFields/TextField';
import { Button } from '../../components/button/Button';

const initialFormState = {};

const BUTTON_TYPE = {
    SUBMIT: 'submit',
};

const ScoresList = () => {
    const [formState, setFormState] = useReducer(
        (newState) => ({ ...formState, ...newState }),
        initialFormState,
    );

    return (
        <Fragment>
            <div>
                <InputField name={'NAME'} />
                <DropdownSelectField name={'SELECT TASK'} values={[]} />
                <TextField name={'DESCRIPTION'} text={'LORE IPSUM'} />
                <InputField name={'SOLUTION CODE'} />
                <Button type={BUTTON_TYPE.SUBMIT} onClick={() => null} />
            </div>
        </Fragment>
    );
};

export default ScoresList;
