import React, { Fragment, useEffect, useMemo, useReducer, useState } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import { isEmpty } from 'lodash';

import { InputField } from '../../components/formFields/InputField';
import { DropdownSelectField } from '../../components/formFields/DropdownSelectField';
import { Button } from '../../components/button/Button';
import { FormFooter } from '../../components/formFields/FormFooter';
import LoaderScreen from '../../components/loader/LoaderScreen';

import { requestCodeTasks, requestPostCodeSubmission } from './codeSubmissionApi';

import 'react-notifications/lib/notifications.css';
import styles from './CodeSubmissionForm.module.scss';

const requirement =
    'Provide task solution implemented with C#. Please implement cod ein such structure: code should be in static class called "Solution" which contained method called "GetResult". This method should return int[]';

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

const getSolutionSubmissionFormValidationErrors = (formState) => {
    const { userName, codeTaskId, taskSolution } = formState;
    let errors = {};

    if (isEmpty(userName.trim())) {
        errors['userName'] = 'Please fill in your user name';
    } else {
        errors['userName'] = undefined;
    }
    if (!Boolean(codeTaskId)) {
        errors['codeTask'] = 'Please choose the task';
    } else {
        errors['codeTask'] = undefined;
    }
    if (isEmpty(taskSolution.trim())) {
        errors['taskSolution'] = 'Please provide tasks implementation';
    } else {
        errors['taskSolution'] = undefined;
    }

    return errors;
};

const NOTIFICATION_TYPE = {
    SUCCESS: 'success',
    COMPUTATION_ERROR: 'computationError',
    WRONG_SOLUTION: 'wrongSolution',
};

const emitNotification = (type) => {
    switch (type) {
        case 'success':
            NotificationManager.success('Task Submitted Successfully');
            break;
        case 'computationError':
            NotificationManager.warning(
                'Compilation error has occurred or you code if in wrong format',
            );
            break;
        case 'wrongSolution':
            NotificationManager.error('Unfortunately the solution is wrong');
            break;
    }
};

const CodeSubmissionForm = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [codeTasks, setCodeTasks] = useState([]);
    const [computationError, setCmputationError] = useState('');
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
        setFormErrors(getSolutionSubmissionFormValidationErrors(formState));

        if (
            isEmpty(formErrors.codeTaskId) ||
            isEmpty(formErrors.codeTaskId) ||
            isEmpty(formErrors.codeTaskId)
        ) {
            const response = await requestPostCodeSubmission(formState);

            if (!isEmpty(response.computationErrorMessage)) {
                emitNotification(NOTIFICATION_TYPE.COMPUTATION_ERROR);
            } else if (response.isSucesss) {
                emitNotification(NOTIFICATION_TYPE.SUCCESS);
            } else {
                emitNotification(NOTIFICATION_TYPE.WRONG_SOLUTION);
            }
            setCmputationError(response.computationErrorMessage);
        }
    };

    return (
        <Fragment>
            <LoaderScreen dim isLoading={isLoading} />
            <div className={styles.formContainer}>
                <InputField
                    name={'NAME'}
                    onChange={handleNameInputChange}
                    error={formErrors.userName}
                />
                <DropdownSelectField
                    name={'SELECT TASK'}
                    placeholder={'Search...'}
                    options={taskSelectOptions}
                    error={formErrors.codeTaskId}
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
                    disabled
                    isTextarea
                    rows={5}
                    name={'REQUIREMENTS'}
                    value={requirement}
                />
                <InputField
                    isTextarea
                    name={'SOLUTION CODE'}
                    placeholder={'Here goes tasks implementation in C# ...'}
                    error={formErrors.taskSolution}
                    onChange={handleSolutionInputChange}
                />
                <FormFooter>
                    <Button
                        color={'primary'}
                        text={'SUBMIT'}
                        type={BUTTON_TYPE.SUBMIT}
                        onClick={handleSubmit}
                    />
                </FormFooter>
            </div>
            <InputField
                disabled
                isTextarea
                rows={4}
                name={'SYSTEM CONSOLE'}
                value={computationError}
            />

            <NotificationContainer />
        </Fragment>
    );
};

export default CodeSubmissionForm;
