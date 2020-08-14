import React, { Component } from 'react';
import {
    Collapse,
    Container,
    Navbar,
    NavbarBrand,
    NavbarToggler,
    NavItem,
    NavLink,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import classnames from 'classnames';

import styles from './NavMenu.module.scss';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed,
        });
    }

    render() {
        return (
            <header>
                <Navbar
                    dark
                    className={classnames(
                        styles.navBar,
                        'navbar-expand-md navbar-toggleable-sm ng-white box-shadow mb-3',
                    )}
                >
                    <Container>
                        <NavbarBrand tag={Link} to='/'>
                            COGNIZANT CHALANGE
                        </NavbarBrand>

                        <NavbarToggler onClick={this.toggleNavbar} className='mr-2' />

                        <Collapse
                            className='d-sm-inline-flex flex-sm-row-reverse'
                            isOpen={!this.state.collapsed}
                            navbar
                        >
                            <ul className='navbar-nav flex-grow'>
                                <NavItem>
                                    <NavLink
                                        tag={Link}
                                        className={styles.navLink}
                                        to='/code-submission'
                                    >
                                        SOLVE
                                    </NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className={styles.navLink} to='/'>
                                        TOP SCORES
                                    </NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
