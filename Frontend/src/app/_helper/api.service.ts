import { Injectable } from "@angular/core";

interface ApiQueryObject {
    name: string;
    value: string[] | string | number | null;
}

@Injectable({
    providedIn: 'root'
})
export class ApiHelper {
    public static queryBuilder(query: ApiQueryObject[]): string {
        if (query.length < 0) return '';
        let queryLength: number = query.length;
        let result: string = '';

        query.forEach((param, index) => {
            if (index === 0) result += '?';

            if (param.value === null) return;

            if (Array.isArray(param.value)) {
                let length = param.value.length;

                result += param.value.reduce(
                    (prev, current, index, array) => {
                        prev += `${param.name}=${current}`;
                        if (index < length - 1) prev += '&';
                        return prev;
                    }, ''
                );
            } else {
                result += `${param.name}=${param.value}`;
            }

            if (index < queryLength - 1) result += '&';
        });

        return result;
    }
}
