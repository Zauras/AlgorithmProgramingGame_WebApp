import React, { Fragment, useEffect, useMemo, useReducer, useState } from 'react';
import { isEmpty } from 'lodash';

import { InputField } from '../../components/formFields/InputField';
import { DropdownSelectField } from '../../components/formFields/DropdownSelectField';
import { Button } from '../../components/button/Button';
import { FormFooter } from '../../components/formFields/FormFooter';
import LoaderScreen from '../../components/loader/LoaderScreen';

import { requestCodeTasks, requestPostCodeSubmission } from './codeSubmissionApi';
import styles from './CodeSubmissionForm.module.scss';

const BUTTON_TYPE = {
    SUBMIT: 'submit',
};

const initFormState = {
    userName: '',
    codeTask: undefined,
    taskSolution: '',
};

const initFormErrors = {
    userName: undefined,
    codeTaskId: undefined,
    taskSolution: undefined,
};

const validateSolutionSubmissionForm = (formState) => {
    const { userName, codeTask, taskSolution } = formState;
    let errors = {};

    if (isEmpty(userName.trim())) {
        errors[userName] = 'Please fill in your user name';
    }
    if (!Boolean(codeTask)) {
        errors[codeTask] = 'Please choose the task';
    }
    if (isEmpty(taskSolution.trim())) {
        errors[taskSolution] = 'Please provide tasks implementation';
    }

    return errors;
};

const CodeSubmissionForm = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [codeTasks, setCodeTasks] = useState([]);
    const [selectedTaskId, setSelectedTaskId] = useState(undefined);
    const [formState, setFormState] = useReducer(
        (formState, newState) => ({ ...formState, ...newState }),
        initFormState,
        (initFormState) => initFormState,
    );
    const [formErrors, setFormErrors] = useReducer(
        (formErrors, newErrors) => ({ ...formErrors, ...newErrors }),
        initFormState,
        (initFormErrors) => initFormErrors,
    );

    const fetchCodeTasks = async () => {
        setIsLoading(true);
        try {
            const response = await requestCodeTasks();
            Boolean(response) ? setCodeTasks(response) : setCodeTasks([]);
        } catch (error) {
            setCodeTasks([]);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        (async () => fetchCodeTasks())();
    }, []);

    const taskSelectOptions = useMemo(
        () =>
            codeTasks.map((task) => ({
                value: task.codeTaskId,
                label: task.name,
            })),
        [codeTasks],
    );

    const handleTaskSelection = ({ value: codeTaskId }) => {
        setSelectedTaskId(codeTaskId);
        setFormState({ codeTaskId });
    };

    const taskDescription = useMemo(() => {
        const selectedTask = codeTasks.find((task) => task.codeTaskId === selectedTaskId);
        return Boolean(selectedTask) ? selectedTask.description : '';
    }, [codeTasks, selectedTaskId]);

    const handleNameInputChange = (event) => setFormState({ userName: event.target.value });

    const handleSolutionInputChange = (event) => setFormState({ taskSolution: event.target.value });

    const handleSubmit = async () => {
        // TODO: Add form validation
        setFormErrors(validateSolutionSubmissionForm(formState));
        const response = await requestPostCodeSubmission(formState);
        // success pop-up
    };

    return (
        <Fragment>
            <LoaderScreen dim isLoading={isLoading} />
            <div className={styles.formContainer}>
                <InputField name={'NAME'} onChange={handleNameInputChange} />
                <DropdownSelectField
                    name={'SELECT TASK'}
                    placeholder={'Search...'}
                    options={taskSelectOptions}
                    onChange={handleTaskSelection}
                />
                <InputField
                    disabled
                    placeholder={'Please select the task ...'}
                    isTextarea
                    rows={6}
                    name={'DESCRIPTION'}
                    value={taskDescription}
                />
                <InputField
                    isTextarea
                    name={'SOLUTION CODE'}
                    placeholder={'Here goes tasks implementation in C# ...'}
                    onChange={handleSolutionInputChange}
                />
                <FormFooter>
                    <Button text={'SUBMIT'} type={BUTTON_TYPE.SUBMIT} onClick={handleSubmit} />
                </FormFooter>
            </div>
        </Fragment>
    );
};

export default CodeSubmissionForm;
