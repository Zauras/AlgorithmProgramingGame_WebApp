import React, { Fragment, useEffect, useState } from 'react';
import Loader from 'react-loader-spinner';

import 'react-loader-spinner/dist/loader/css/react-spinner-loader.css';
import './LoaderScreen.scss';

const minSpinTime: number = 500;
const voidFunc = () => {};

const LoaderScreen = ({ isLoading = true, dim = false, onLoadUnlock = voidFunc }) => {
    const [isLoaderLocked, setIsLoaderLocked] = useState<boolean>(false);

    useEffect(() => {
        if (isLoading) {
            (async () => await startSpinLockTimeout())();
        }
    });

    const startSpinLockTimeout = async () => {
        setIsLoaderLocked(true);
        await setTimeout(() => {
            onLoadUnlock();
            setIsLoaderLocked(false);
        }, minSpinTime);
    };

    const loaderScreenClassName = dim ? 'loader-screen loader-screen__dim' : 'loader-screen';

    return (
        <Fragment>
            {(isLoading || isLoaderLocked) && (
                <div className={loaderScreenClassName}>
                    <div className='loader-align'>
                        <Loader type='Puff' color='#00BFFF' height={100} width={100} />
                    </div>
                </div>
            )}
        </Fragment>
    );
};

LoaderScreen.displayName = 'LoaderScreen';
export default LoaderScreen;
