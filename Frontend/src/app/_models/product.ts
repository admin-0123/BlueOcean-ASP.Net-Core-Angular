export interface Product {
    id: string;
    title: string;
    price: number;
    images: ProductImage[];
    description?: string;
    attributes?: ProductAttribute[];
}

export interface ProductImage {
    id: number;
    primary: boolean;
    url: string;
}

export interface ProductAttribute {
    id: number;
    name: string;
    value: string;
}

export interface ProductInCart {
    id: string;
    title: string;
    price: number;
    images: string[];
    quantity: number;
}

export interface Category {
    name: string;
    title: string;
}
