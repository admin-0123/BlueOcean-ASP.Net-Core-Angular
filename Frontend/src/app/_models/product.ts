export interface Product {
    id: string;
    title: string;
    price: number;
    images: string[];
    description?: string;
    attributes?: ProductAttribute[];
}

export interface ProductInCart extends Product {
    quantity: number;
}

export interface ProductAttribute {
    id: number;
    name: string;
    value: string;
}

export interface Category {
    name: string;
    title: string;
}
