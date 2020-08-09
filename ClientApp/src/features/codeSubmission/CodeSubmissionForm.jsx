import React, { useReducer } from 'react';

import { InputField } from '../../components/formFields/InputField';
import { DropdownSelectField } from '../../components/formFields/DropdownSelectField';
import { Button } from '../../components/button/Button';
import { FormFooter } from '../../components/formFields/FormFooter';

import { requestPostCodeSubmission } from './codeSubmissionApi';
import styles from './CodeSubmissionForm.module.scss';

const initialFormState = {};

const BUTTON_TYPE = {
    SUBMIT: 'submit',
};

const CodeSubmissionForm = () => {
    const [formState, setFormState] = useReducer(
        (newState) => ({ ...formState, ...newState }),
        initialFormState,
    );

    const handleSubmit = async () => {
        const response = await requestPostCodeSubmission(formState);
        // success pop-up
    };

    return (
        <div className={styles.formContainer}>
            <InputField name={'NAME'} />
            <DropdownSelectField name={'SELECT TASK'} placeholder={'Search...'} values={[]} />
            <InputField disabled name={'DESCRIPTION'} value={'LORE IPSUM'} />
            <InputField isTextarea name={'SOLUTION CODE'} />
            <FormFooter>
                <Button text={'SUBMIT'} type={BUTTON_TYPE.SUBMIT} onClick={handleSubmit} />
            </FormFooter>
        </div>
    );
};

export default CodeSubmissionForm;
