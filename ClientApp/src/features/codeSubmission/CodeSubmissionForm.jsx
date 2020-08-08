import React, { Fragment, useReducer } from 'react';
import { InputField } from '../../components/form/formFields/InputField';
import { DropdownSelectField } from '../../components/form/formFields/DropdownSelectField';
import { Button } from '../../components/button/Button';

const initialFormState = {};

const BUTTON_TYPE = {
    SUBMIT: 'submit',
};

const CodeSubmissionForm = () => {
    const [formState, setFormState] = useReducer(
        (newState) => ({ ...formState, ...newState }),
        initialFormState,
    );

    return (
        <Fragment>
            <div>
                <InputField name={'NAME'} />
                <DropdownSelectField name={'SELECT TASK'} values={[]} />
                <InputField disabled name={'SOLUTION CODE'} value={'LORE IPSUM'} />
                <InputField name={'SOLUTION CODE'} />
                <Button text={'SUBMIT'} type={BUTTON_TYPE.SUBMIT} onClick={() => null} />
            </div>
        </Fragment>
    );
};

export default CodeSubmissionForm;
