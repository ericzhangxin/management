import { Injectable } from '@angular/core';

//Using the new HttpClientModule now. If you're still on < Angular 4.3 see the 
//data.service.ts file instead (simplify rename it to the name 
//of this file to use it instead)
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map'; 
import 'rxjs/add/operator/catch';

import { ICustomer, IGender, IColor, IPagedResults, ICustomerResponse } from '../shared/interfaces';

@Injectable()
export class DataService {
  
    baseUrl: string = 'http://localhost:46692/records';
    baseGendersUrl: string = 'http://localhost:46692/api/genders'
    baseColorsUrl: string = 'http://localhost:46692/api/colors'

    constructor(private http: HttpClient) { 

    }
    
    getCustomers() : Observable<ICustomer[]> {
        return this.http.get<ICustomer[]>(this.baseUrl)
                   .map((customers: ICustomer[]) => {
                       return customers;
                   })
                   .catch(this.handleError);
    }

    getCustomersPage(page: number, pageSize: number) : Observable<IPagedResults<ICustomer[]>> {
        return this.http.get<ICustomer[]>(`${this.baseUrl}/page/${page}/${pageSize}`, {observe: 'response'})
                    .map((res) => {
                        //Need to observe response in order to get to this header (see {observe: 'response'} above)
                        const totalRecords = +res.headers.get('x-inlinecount');
                        let customers = res.body as ICustomer[];
                        return {
                            results: customers,
                            totalRecords: totalRecords
                        };
                    })
                    .catch(this.handleError);
    }
    
    getCustomer(id: string) : Observable<ICustomer> {
        return this.http.get<ICustomer>(this.baseUrl + '/' + id)
                   .catch(this.handleError);
    }

    insertCustomer(customer: ICustomer) : Observable<ICustomer> {
        return this.http.post<ICustomerResponse>(this.baseUrl, customer)
                   .map((data) => {
                       if(data)
                            console.log('insertCustomer status: succesful');
                       return data;
                   })
                   .catch(this.handleError);
    }
   
    updateCustomer(customer: ICustomer) : Observable<ICustomer> {
        return this.http.put<ICustomerResponse>(this.baseUrl + '/' + customer.Id, customer) 
                   .map((data) => {
                       if(data)
                            console.log('updateCustomer status: successful');
                       return data;
                   })
                   .catch(this.handleError);
    }

    deleteCustomer(id: number) : Observable<boolean> {
        return this.http.delete<boolean>(this.baseUrl + '/' + id)
                   .catch(this.handleError);
    }
   
    getGenders(): Observable<IGender[]> {
        return this.http.get<IGender[]>(this.baseGendersUrl)
                   .catch(this.handleError);
    }

    
    getColors(): Observable<IColor[]> {
        return this.http.get<IColor[]>(this.baseColorsUrl)
                   .catch(this.handleError);
    }

    private handleError(error: HttpErrorResponse) {
        console.error('server error:', error); 
        if (error.error instanceof Error) {
          let errMessage = error.error.message;
          return Observable.throw(errMessage);
          // Use the following instead if using lite-server
          //return Observable.throw(err.text() || 'backend server error');
        }
        return Observable.throw(error || 'Node.js server error');
    }

}
