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
    price: boolean;
    url: string;
}

export interface ProductAttribute {
    id: number;
    name: string;
    value: string;
}

export interface ProductInCart extends Product {
    quantity: number;
}

export interface Category {
    name: string;
    title: string;
}
