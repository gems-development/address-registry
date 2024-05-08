import React from 'react';

const AddressPanel = () => {
  return (
    <div className="address-panel bg-light p-3 position-fixed top-0 start-0 bottom-0" style={{ width: '300px', zIndex: '1', display: 'none' }}>
      <h3>Адреса</h3>
      <p>Адрес в муниципальном делении</p>
      <p>Адрес в административном делении</p>
      <p>ОКАТО</p>
      <p>ОКТМО</p>
      <p>Почтовый индекс</p>
    </div>
  );
};

export default AddressPanel;