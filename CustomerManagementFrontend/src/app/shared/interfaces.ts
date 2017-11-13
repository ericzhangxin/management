import { ModuleWithProviders } from '@angular/core';

export interface ICustomer {
    Id?: number;
    LastName: string;
    FirstName: string;
    Gender: number;
    FavoriteColor:number;
    DateOfBirth: Date
    
}

export interface IGender {
    Gender: string;
}

export interface IColor {
    Color: string;
}


export interface IRouting {
    routes: ModuleWithProviders,
    components: any[]
}

export interface IPagedResults<T> {
    totalRecords: number;
    results: T;
}

export interface ICustomerResponse {
    customer: ICustomer;
    status: boolean;
    error: string;
}