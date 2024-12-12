import React, { useEffect, useState } from "react";
import api from "../services/api";
import "./ProductsPage.css";

const ProductsPage = () => {
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [view, setView] = useState("products"); // Switch between products and categories
  const [editItem, setEditItem] = useState(null); // Item being edited
  const [newItem, setNewItem] = useState({
    name: "",
    description: "",
    price: "",
    stock: "",
    categoryId: "",
    isProduct: true,
  });

  useEffect(() => {
    fetchProducts();
    fetchCategories();
  }, []);

  const fetchProducts = async () => {
    try {
      const response = await api.get("/product");
      setProducts(response.data);
    } catch (err) {
      console.error("Error fetching products:", err);
    }
  };

  const fetchCategories = async () => {
    try {
      const response = await api.get("/categories");
      setCategories(response.data);
    } catch (err) {
      console.error("Error fetching categories:", err);
    }
  };

  // const handleAddOrEdit = async (e) => {
  //   e.preventDefault();
  //   try {
  //     if (editItem) {
  //       if (newItem.isProduct) {
  //         // Update Product
  //         await api.put("/product", {
  //           productid: editItem.productid,
  //           productname: newItem.name,
  //           categoryid: newItem.categoryId,
  //           unitprice: newItem.price,
  //           unitsinstock: newItem.stock,
  //         });
  //         fetchProducts();
  //       } else {
  //         // Update Category
  //         await api.put("/categories", {
  //           categoryid: editItem.categoryid,
  //           categoryname: newItem.name,
  //           description: newItem.description,
  //         });
  //         fetchCategories();
  //       }
  //     } else {
  //       if (newItem.isProduct) {
  //         // Add Product
  //         await api.post("/product", {
  //           productname: newItem.name,
  //           categoryid: newItem.categoryId,
  //           unitprice: newItem.price,
  //           unitsinstock: newItem.stock,
  //         });
  //         fetchProducts();
  //       } else {
  //         // Add Category
  //         await api.post("/categories", {
  //           categoryname: newItem.name,
  //           description: newItem.description,
  //         });
  //         fetchCategories();
  //       }
  //     }
  //     resetForm();
  //   } catch (err) {
  //     console.error("Error saving item:", err);
  //   }
  // };

  const handleAddOrEdit = async (e) => {
    e.preventDefault();
    try {
      if (editItem) {
        // Handle Edit
        if (newItem.isProduct) {
          // Update Product
          await api.put("/product", {
            productid: editItem.productid,
            productname: newItem.name,
            categoryid: newItem.categoryId,
            unitprice: newItem.price,
            unitsinstock: newItem.stock,
          });
          fetchProducts();
        } else {
          // Update Category
          await api.put("/categories", {
            categoryid: editItem.categoryid,
            categoryname: newItem.name,
            description: newItem.description,
          });
          fetchCategories();
        }
      } else {
        // Handle Add
        if (newItem.isProduct) {
          // Add Product
          await api.post("/product", {
            productname: newItem.name,
            categoryid: newItem.categoryId,
            unitprice: newItem.price,
            unitsinstock: newItem.stock,
          });
          fetchProducts();
        } else {
          // Add Category
          await api.post("/categories", {
            categoryname: newItem.name,
            description: newItem.description,
          });
          fetchCategories();
        }
      }
      resetForm();
    } catch (err) {
      console.error("Error saving item:", err);
    }
  };

  const handleEdit = (item, isProduct) => {
    setEditItem(item);
    setNewItem({
      name: isProduct ? item.productname : item.categoryname,
      description: isProduct ? "" : item.description,
      price: isProduct ? item.unitprice : "",
      stock: isProduct ? item.unitsinstock : "",
      categoryId: isProduct ? item.categoryid : "",
      isProduct,
    });
  };

  const handleDelete = async (id, isProduct) => {
    try {
      if (isProduct) {
        await api.delete(`/product/${id}`);
        fetchProducts();
      } else {
        await api.delete(`/categories/${id}`);
        fetchCategories();
      }
    } catch (err) {
      console.error("Error deleting item:", err);
    }
  };

  const resetForm = () => {
    setNewItem({
      name: "",
      description: "",
      price: "",
      stock: "",
      categoryId: "",
      isProduct: true,
    });
    setEditItem(null);
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    window.location.href = "/";
  };

  return (
    <div className="container">
      <button className="logout-btn" onClick={handleLogout}>
        Logout
      </button>
      <h2>
        {view === "products" ? "Products Management" : "Categories Management"}
      </h2>
      <div className="view-toggle">
        <button onClick={() => setView("products")}>View Products</button>
        <button onClick={() => setView("categories")}>View Categories</button>
      </div>

      {/* <form onSubmit={handleAddOrEdit} className="add-form">
        <h3>
          {editItem ? "Edit" : "Add"}{" "}
          {newItem.isProduct ? "Product" : "Category"}
        </h3>
        <input
          type="text"
          placeholder="Name"
          value={newItem.name}
          onChange={(e) => setNewItem({ ...newItem, name: e.target.value })}
          required
        />
        {!newItem.isProduct && (
          <input
            type="text"
            placeholder="Description"
            value={newItem.description}
            onChange={(e) =>
              setNewItem({ ...newItem, description: e.target.value })
            }
            required
          />
        )}
        {newItem.isProduct && (
          <>
            <input
              type="number"
              placeholder="Price"
              value={newItem.price}
              onChange={(e) =>
                setNewItem({ ...newItem, price: e.target.value })
              }
              required
            />
            <input
              type="number"
              placeholder="Stock"
              value={newItem.stock}
              onChange={(e) =>
                setNewItem({ ...newItem, stock: e.target.value })
              }
              required
            />
            <input
              type="number"
              placeholder="Category ID"
              value={newItem.categoryId}
              onChange={(e) =>
                setNewItem({ ...newItem, categoryId: e.target.value })
              }
              required
            />
          </>
        )}
        <button type="submit">{editItem ? "Update" : "Add"}</button>
        {editItem && <button onClick={resetForm}>Cancel</button>}
      </form> */}

      {/* Dynamic Add/Edit Form */}
      <form onSubmit={handleAddOrEdit} className="add-form">
        <h3>
          {editItem ? "Edit" : "Add"}{" "}
          {view === "products" ? "Product" : "Category"}
        </h3>
        <input
          type="text"
          placeholder="Name"
          value={newItem.name}
          onChange={(e) => setNewItem({ ...newItem, name: e.target.value })}
          required
        />
        {view === "categories" && (
          <input
            type="text"
            placeholder="Description"
            value={newItem.description}
            onChange={(e) =>
              setNewItem({ ...newItem, description: e.target.value })
            }
            required
          />
        )}
        {view === "products" && (
          <>
            <input
              type="number"
              placeholder="Price"
              value={newItem.price}
              onChange={(e) =>
                setNewItem({ ...newItem, price: e.target.value })
              }
              required
            />
            <input
              type="number"
              placeholder="Stock"
              value={newItem.stock}
              onChange={(e) =>
                setNewItem({ ...newItem, stock: e.target.value })
              }
              required
            />
            <input
              type="number"
              placeholder="Category ID"
              value={newItem.categoryId}
              onChange={(e) =>
                setNewItem({ ...newItem, categoryId: e.target.value })
              }
              required
            />
          </>
        )}
        <button type="submit">{editItem ? "Update" : "Add"}</button>
        {editItem && <button onClick={resetForm}>Cancel</button>}
      </form>

      <div className="list-table">
        {view === "products" ? (
          <table>
            <thead>
              <tr>
                <th>Name</th>
                <th>Price</th>
                <th>Stock</th>
                <th>Category ID</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {products.map((product) => (
                <tr key={product.productid}>
                  <td>{product.productname}</td>
                  <td>{product.unitprice}</td>
                  <td>{product.unitsinstock}</td>
                  <td>{product.categoryid}</td>
                  <td>
                    <button onClick={() => handleEdit(product, true)}>
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(product.productid, true)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <table>
            <thead>
              <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Description</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {categories.map((category) => (
                <tr key={category.categoryid}>
                  <td>{category.categoryid}</td>
                  <td>{category.categoryname}</td>
                  <td>{category.description}</td>
                  <td>
                    <button onClick={() => handleEdit(category, false)}>
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(category.categoryid, false)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default ProductsPage;
